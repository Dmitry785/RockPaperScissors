using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

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
            CurrentItemImagePath = string.Empty;
            BattleVersusImagePath = string.Empty;
            OpponentItemImagePath = string.Empty;
            BattleResult = string.Empty;
            IsNoBattleRightNow = false;
            CurrentItemImagePath = CacheManager.GetImagePathByItem(item);
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            try
            {
                BattleVersusImagePath = CacheManager.GetBattleVersusImagePath_Battle();
                Item opponentItem = await Task.Run(() => GetOpponentMove(), cts.Token);
                var res = item.Battle(opponentItem);
                BattleResult = res.ToString();
                OpponentItemImagePath = CacheManager.GetImagePathByItem(opponentItem);
                if (res is RockPaperScissors.BattleResult.Draw)
                    BattleVersusImagePath = CacheManager.GetBattleVersusImagePath_Draw();
                else if (res is RockPaperScissors.BattleResult.Win)
                    BattleVersusImagePath = CacheManager.GetBattleVersusImagePath_Win();
                else
                    BattleVersusImagePath = CacheManager.GetBattleVersusImagePath_Fail();
            }
            catch
            {
                MessageBox.Show("Ошибка во время игры");
            }
            IsNoBattleRightNow = true;
        }
        private Item GetOpponentMove()
        {
            Task.Delay(1000).Wait();
            var opponentItem = ItemGenerator.Generate();
            return opponentItem;
        }
    }
    #region Other
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
    #endregion
}
