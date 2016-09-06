using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace FileWatchDemo
{
    public class FileWatch
    {
        SendOrPostCallback m_callback;
        SynchronizationContext m_context;

        //开始监视
        public void StartWatch()
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Environment.CurrentDirectory + "\\Test";
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            // Only watch text files.
            watcher.Filter = "*.txt";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
          
            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        public void SetCallBack(SendOrPostCallback callback)
        {
            m_callback = callback;
            m_context = SynchronizationContext.Current;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            string strinfo = "";
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                strinfo = string.Format("File:{0}  改变", e.FullPath);
            }
            else if (e.ChangeType == WatcherChangeTypes.Created)
            {
                strinfo = string.Format("File:{0}  创建", e.FullPath);
            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                strinfo = string.Format("File:{0}  删除", e.FullPath);
            }

            if(strinfo!="")
            {
                m_context.Post(m_callback, (object)strinfo);
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            string strinfo = "";

            if(e.ChangeType == WatcherChangeTypes.Renamed)
            {
                strinfo = string.Format("File:{0}  重命名", e.FullPath);
            }

            if (strinfo != "")
            {
                m_context.Post(m_callback, (object)strinfo);
            }
        }
    }
}
