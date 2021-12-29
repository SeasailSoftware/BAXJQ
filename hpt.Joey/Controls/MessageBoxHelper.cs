using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joey.UserControls
{
    public class MessageBoxHelper
    {
        /// <summary>
        /// 提示对话框
        /// </summary>
        /// <param name="msg">消息内容</param>
        public static void Info(string msg)
        {
            new JMessageButton(MessageBoxStyle.info, msg).ShowDialog();
        }

        /// <summary>
        /// 错误对话框
        /// </summary>
        /// <param name="msg">消息内容</param>
        public static void Error(string msg)
        {
            new JMessageButton(MessageBoxStyle.error, msg).ShowDialog();
        }

        /// <summary>
        /// 询问对话框
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Question(string msg)
        {
            return new JMessageButton(MessageBoxStyle.question, msg).ShowDialog();
        }
    }
}
