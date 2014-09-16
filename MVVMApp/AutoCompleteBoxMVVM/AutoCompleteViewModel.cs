using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Collections.ObjectModel;   // ObservableCollection
using System.Windows.Input;             // ICommand
using System.Runtime.CompilerServices;  // CallerMemberName属性
using System.Windows.Controls;          // AutoCompleteFilterPredicate

namespace AutoCompleteBoxMVVM
{
    public class AutoCompleteViewModel : ViewModelBase
    {
        /// <summary>
        /// オートコンプリートのソースとなるプロパティ
        /// </summary>
        private ObservableCollection<Ringo> _ringoSource;
        public ObservableCollection<Ringo> RingoSource
        {
            get
            {
                if (_ringoSource == null)
                {
                    _ringoSource = new ObservableCollection<Ringo>()
                    {
                        new Ringo(){ ID = 1, Name = "シナノゴールド" },
                        new Ringo(){ ID = 2, Name = "シナノドルチェ" },
                        new Ringo(){ ID = 3, Name = "ジョナゴールド" }
                    };
                }
                return _ringoSource;
            }
        }

        /// <summary>
        /// フィルターを実装するプロパティ
        /// </summary>
        public AutoCompleteFilterPredicate<object> RingoFilter
        {
            get { return (searchText, obj) => (obj as Ringo).Name.Contains(searchText); }
        }

        /// <summary>
        /// オートコンプリートで選択されたもの
        /// なお、すべて直接入力されてもソースと一致しているものであれば、
        /// ソースの値がセットされる
        /// </summary>
        private Ringo _selectedRingo;
        public Ringo SelectedRingo
        {
            get { return _selectedRingo; }
            set
            {
                _selectedRingo = value;
            }
        }

        /// <summary>
        /// オートコンプリートに関わらず、入力した値
        /// </summary>
        private string _selectedRingoName;
        public string SelectedRingoName
        {
            get { return _selectedRingoName; }
            set { _selectedRingoName = value; }
        }


        /// <summary>
        /// 追加ボタンで追加されるDataGridの内容
        /// </summary>
        private ObservableCollection<Ringo> _addedRingo;
        public ObservableCollection<Ringo> AddedRingo
        {
            get
            {
                if (_addedRingo == null)
                {
                    _addedRingo = new ObservableCollection<Ringo>();
                }
                return _addedRingo;
            }
            set
            {
                _addedRingo = value;
                RaisePropertyChanged();
            }
        }



        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new Microsoft.TeamFoundation.MVVM.RelayCommand(ExecuteAddCommand);
                }
                return _addCommand;
            }
        }

        private void ExecuteAddCommand()
        {
            //  直接入力された時はSelectedRingoがnullで、SelectedRingoNameに入力された値が入ってくる
            AddedRingo.Add(SelectedRingo ?? new Ringo { ID = 4, Name = SelectedRingoName });
        }


        protected override void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            RaisePropertyChanged(propertyName);
        }
    }
}
