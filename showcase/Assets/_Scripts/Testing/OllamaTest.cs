using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Models.Chat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.game.testing
{
    public class OllamaTest : MonoBehaviour
    {
        public enum State
        {
            InitializingOllama,
            LoadingModel,
            Done,
        }

        public const int OLLAMA_PORT = 7776;
        public const string OLLAMA_MODEL = "deepseek-r1:7b";
        public static readonly string OllamaUrl = $"http://localhost:{OLLAMA_PORT}";

        private static readonly string s_doNotUse = "http://localhost:7776";

        string m_chatHistory = string.Empty;
        string m_text = string.Empty;

        State m_state = State.InitializingOllama;
        OllamaApiClient m_client;
        Uri m_uri;
        Chat m_chat;

        bool m_canWrite = true;
        float m_modelLoadPercentage;
        string m_modelLoadState = string.Empty;

        StringBuilder m_receiveCache;
        volatile string m_receive;

        private void Start()
        {
            Application.quitting -= OnQuit;
            Application.quitting += OnQuit;

            System.Environment.SetEnvironmentVariable("OLLAMA_HOST", $"127.0.0.1:{OLLAMA_PORT}");
            m_uri = new(OllamaUrl);
            m_client = new(m_uri);

            BootstrapStartupSequence();
        }

        private void OnQuit()
        {
            ForceStopServer();
        }

        void BootstrapStartupSequence()
        {
            m_state = State.InitializingOllama;
            Task.Run(() => IsOllamaRunning()).ContinueWith(OnVerificationCompleted);
        }

        private void OnVerificationCompleted(Task<bool> task)
        {
            bool ollamaAlreadyRunning = task.Result;

            if (ollamaAlreadyRunning)
            {
                Run();
                return;
            }

            BootstrapServer();
        }

        async Task<bool> IsOllamaRunning()
        {
            try
            {
                IEnumerable<Model> models = await m_client.ListLocalModelsAsync();
                return models.Any();
            }

            catch
            {
                return false;
            }
        }

        void BootstrapServer() 
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C start ollama serve",
                UseShellExecute = false,
                CreateNoWindow = true,
            });

            Run();
        }

        void Run()
        {
            Task.Run(() => RunAsync()).ContinueWith(Initialize);
        }

        async Task RunAsync()
        {
            m_state = State.LoadingModel;
            await foreach (var status in m_client.PullModelAsync(OLLAMA_MODEL))
            {
                m_modelLoadPercentage = (float)status.Percent;
                m_modelLoadState = status.Status;
            }
        }

        void Initialize(Task task)
        {
            if (!task.IsCompletedSuccessfully)
                return;

            m_client.SelectedModel = OLLAMA_MODEL;
            m_chat = new Chat(m_client);
            m_state = State.Done;
        }

        void ForceStopServer()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C netstat -ano | findstr :{OLLAMA_PORT}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                var lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 5 && int.TryParse(parts.Last(), out int pid))
                    {
                        try
                        {
                            Process proc = Process.GetProcessById(pid);
                            if (proc.ProcessName.Contains("ollama", StringComparison.OrdinalIgnoreCase))
                            {
                                proc.Kill();
                                UnityEngine.Debug.Log($"Stopped Ollama on port {OLLAMA_PORT} (PID: {pid})");
                            }
                            else
                            {
                                UnityEngine.Debug.Log($"Skipping process {proc.ProcessName} (PID: {pid}) - Not Ollama!");
                            }
                        }
                        catch (Exception ex)
                        {
                            UnityEngine.Debug.Log($"Error stopping Ollama: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void OnGUI()
        {
            if (m_state == State.InitializingOllama)
            {
                GUILayout.Label($"Initializing Ollama...");
                return;
            }

            else if (m_state == State.LoadingModel)
            {
                GUILayout.Label($"Pulling model: {m_modelLoadPercentage}% ({m_modelLoadState})");
                return;
            }

            GUILayoutOption[] options = new GUILayoutOption[]
            {
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true)
            };

            GUILayout.BeginVertical("box", options);

            GUILayout.Label(m_chatHistory, GUILayout.ExpandWidth(true));

            GUI.enabled = m_canWrite;

            m_text = GUILayout.TextField(m_text, GUILayout.ExpandWidth(true));

            if (GUILayout.Button("send"))
            {
                m_chatHistory += m_text;
                m_chatHistory += "\n";
                m_chatHistory += "\n";

                Task task = Task.Run(Receive);
                task.ContinueWith(OnReceiveTask);

                m_canWrite = false;
            }

            GUI.enabled = true;

            GUILayout.EndVertical();
        }

        private void OnReceiveTask(Task task)
        {
            if (!task.IsCompletedSuccessfully)
            {
                m_chatHistory += "<color=red>Something wrong happened.</color>";
                m_canWrite = true;
                m_text = string.Empty;
                return;
            }

            m_canWrite = true;
            m_text = string.Empty;

            string result = m_receive;
            int index = result.LastIndexOf('>');

            if (index != -1)
            {
                result = result[(index + 1)..];
            }

            result = result.Trim('\n', ' ');

            m_chatHistory += $"<color=yellow>{result}</color>";
            m_chatHistory += "\n";
            m_chatHistory += "\n";
            //m_chatHistory += result;

            UnityEngine.Debug.Log("received.");
        }

        public async Task Receive()
        {
            m_receiveCache = new();
            await foreach(string prompt in m_chat.SendAsAsync(ChatRole.User, m_text))
            {
                m_receiveCache.Append(prompt);
            }

            m_receive = m_receiveCache.ToString();
        }
    }
}