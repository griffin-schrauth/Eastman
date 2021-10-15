using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace firstApp
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        //create action for command
        Action<object> executeMethod;
        Func<object, bool> canexecuteMethod;
        
        public Command(Action<object> executeMethod , Func<object, bool> canexecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canexecuteMethod = canexecuteMethod;
        }
        //return true so we can execute
        public bool CanExecute(object parameter)
        {    
            return true;
        }
        //execute method
        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }

        
        
    }
}
