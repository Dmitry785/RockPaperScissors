using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private string battleResult = string.Empty;
        private bool isNoBattleRightNow = true;
        public string BattleResult
        {
            get => battleResult;
            set
            {
                battleResult = value;
                OnPropertyChanged();
            }
        }
        private string currentItemImagePath;
        private string battleVersusImagePath;
        private string opponentItemImagePath;
        public string CurrentItemImagePath
        {
            get => currentItemImagePath;
            set
            {
                currentItemImagePath = value;
                OnPropertyChanged();
            }
        }
        public string BattleVersusImagePath
        {
            get => battleVersusImagePath;
            set
            {
                battleVersusImagePath = value;
                OnPropertyChanged();
            }
        }
        public string OpponentItemImagePath
        {
            get => opponentItemImagePath;
            set
            {
                opponentItemImagePath = value;
                OnPropertyChanged();
            }
        }
        public ICommand ChooseRockCommand { get; }
        public ICommand ChoosePaperCommand { get; }
        public ICommand ChooseScissorsCommand { get; }
        public bool IsNoBattleRightNow
        {
            get => isNoBattleRightNow;
            set
            {
                isNoBattleRightNow = value;
                OnPropertyChanged();
            }
        }
        public RockPaperScissorsViewModel()
        {
            ChooseRockCommand = new Command(async () =>
            {
                await StartBattle(new Rock());
            });
            ChoosePaperCommand = new Command(async () =>
            {
                await StartBattle(new Paper());
            });
            ChooseScissorsCommand = new Command(async () =>
            {
                await StartBattle(new Scissors());
            });
        }
        private async Task StartBattle(Item item)
        {
            if (!IsNoBattleRightNow)
                return;
            BattleResult = string.Empty;
            IsNoBattleRightNow = false;
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            try
            {
                BattleResult res = await Task.Run(() => RunBattleTask(item), cts.Token);
                BattleResult = res.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка во время игры");
            }
            IsNoBattleRightNow = true;
        }
        private BattleResult RunBattleTask(Item item)
        {
            var opponentItem = ItemGenerator.Generate();
            Task.Delay(1000);
            return item.Battle(opponentItem);
        }
    }
    public class Command : ICommand
    {
        public Action Execution { get; set; }
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
}
