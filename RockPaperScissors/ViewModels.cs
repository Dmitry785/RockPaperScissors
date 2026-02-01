using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RockPaperScissors
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class RockPaperScissorsViewModel : BaseViewModel
    {
        public string BattleResult { get; set; }
        public ICommand ChooseRockCommand { get; set; }
        public ICommand ChoosePaperCommand { get; set; }
        public ICommand ChooseScissorsCommand { get; set; }
        public RockPaperScissorsViewModel()
        {
            ChooseRockCommand = new Command(() =>
            {
                
            });
        }
        private void StartBattle(Item item)
        {

        }
    }
    public class Command : ICommand
    {
        public Action Execution {  get; set; }
        public Command(Action action)
        {
            Execution = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Execution?.Invoke();
        }
    }
