using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
 
 

namespace DemoApp
{
    public class MefBootstrapper : MefBootstrapperBase<ShellViewModel>
    {
       
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {

             
         
                base.OnStartup(sender, e);
          
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;               
            }
            catch (Exception ex)
            {
               
            }
        }

 
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var list = new HashSet<string>
            {
                       
            };
            var files = Directory.GetFiles(Environment.CurrentDirectory, "*.dll");
            var source = new List<Assembly>
            {
                Assembly.GetEntryAssembly()
            };
            var p = string.Concat(Environment.CurrentDirectory, "\\");
            foreach (string dll in files)
            {
                if (list.Contains(dll.Replace(p, "")))
                    continue;
                try
                {
                    var assembly = Assembly.LoadFile(dll);
                    source.Add(assembly);
                }
                catch (Exception ex)
                {
                    
                }
            }
            return source;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                this.ShowException(ex);
  
        }

         
 

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ShowException(e.Exception);
            e.Handled = true;
        }

      

        private void ShowException(Exception exception)
        {
            if (exception == null)
                return;
            
#if DEBUG
            //AppDispatcher.Invoke(() =>
            //{
            //    if (!_isErrorOpen)
            //    {
            //        _isErrorOpen = true;
            //        IoC.Get<IWindowManager>().ShowWarningWindow("出现错误，请查看日志");

            //        _isErrorOpen = false;
            //    }
            //});
#endif
        }
    }
}
