using System;
using System.Collections.Generic;
using System.Reflection;

namespace NgxLib
{
    public class NgxMessenger
    {
        protected Queue<NgxMessage> MessageQueue { get; set; }
        protected NgxCommandExecutor CommandExecutor { get; set; }
        protected ObjectPool<NgxMessage> MessagePool { get; set; }

        public NgxMessenger()
        {
            MessageQueue = new Queue<NgxMessage>();
            CommandExecutor = new NgxCommandExecutor();
            MessagePool = new ObjectPool<NgxMessage>();
        }

        public NgxMessage Create(long messageKey)
        {
            var message = MessagePool.Get();
            message.MessageKey = messageKey;
            return message;
        }

        public void Send(NgxMessage message)
        {
            MessageQueue.Enqueue(message);
        }

        public void Send(int messageKey, int entity = 0, int sound = 0)
        {
            var msg = Create(messageKey);
            msg.Sound = sound;
            msg.Entity1 = entity;
            MessageQueue.Enqueue(msg);
        }

        public void Flush(NgxContext context)
        {
            while (MessageQueue.Count > 0)
            {
                var message = MessageQueue.Dequeue();
                CommandExecutor.Execute(context, message);
                MessagePool.Release(message);
            }
        }

        public void Register(Assembly assembly)
        {
            var commands = TypeActivator.Activate<INgxCommand>(assembly);
            
            foreach (var command in commands)
            {
                CommandExecutor.Add(command.GetMessageKey(), command);
            }
        }

        public void Unregister()
        {
            CommandExecutor.RemoveAll();
        }
    }
}