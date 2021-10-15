using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace firstApp
{
    public class SendCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        //create action for command
        Action<object> executeMethod;
        
        public SendCommand(Action<object> executeMethod)//command action
        {
            this.executeMethod = executeMethod;        
        }
        //returns true so we can execute
        public bool CanExecute(object parameter)
        {
            return true;
        }
        //execute action we want to take place
        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }



    }
}
