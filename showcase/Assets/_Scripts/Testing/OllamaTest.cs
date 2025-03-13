using OllamaSharp;
using OllamaSharp.Models.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.game.testing
{
    public class OllamaTest : MonoBehaviour
    {
        string chatHistory = string.Empty;
        string text = string.Empty;

        OllamaApiClient client;
        Uri uri;
        Chat chat;

        bool canWrite = true;

        private void Start()
        {
            uri = new("http://localhost:11434");
            client = new(uri);

            client.SelectedModel = "deepseek-r1:7b";

            chat = new Chat(client, "");
        }

        private void OnGUI()
        {
            GUILayoutOption[] options = new GUILayoutOption[]
            {
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true)
            };

            GUILayout.BeginVertical("box", options);

            GUILayout.Label(chatHistory, GUILayout.ExpandWidth(true));

            GUI.enabled = canWrite;

            text = GUILayout.TextField(text, GUILayout.ExpandWidth(true));

            if (GUILayout.Button("send"))
            {
                chatHistory += "\n";
                chatHistory += "\n";
                chatHistory += text;
                chatHistory += "\n";
                chatHistory += "\n";

                Task task = Task.Run(Receive);
                task.ContinueWith(OnReceiveTask);

                canWrite = false;
            }

            GUI.enabled = true;

            GUILayout.EndVertical();
        }

        private void OnReceiveTask(Task task)
        {
            canWrite = true;
            text = string.Empty;
            Debug.Log("received.");
        }

        public async Task Receive()
        {
            await foreach(string prompt in chat.SendAsAsync(ChatRole.User, text))
            {
                chatHistory += prompt;
            }
        }

        private void OnDestroy()
        {
            client.Dispose();
        }
    }
}