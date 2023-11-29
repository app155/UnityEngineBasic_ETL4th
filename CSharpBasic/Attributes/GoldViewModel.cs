using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Attributes
{
    internal class GoldViewModel : INotifyPropertyChanged
    {
        public static GoldViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GoldViewModel();

                return _instance;
            }
        }

        private static GoldViewModel _instance;

        public int Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;

                _value = value;
                OnPropertyChanged();
            }
        }

        private int _value;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ReadGoldModel()
        {
            // local에 있는지 server에 있는지 확인
            // 조건에 맞는 위치에 데이터 읽기 요청하는 코드 작성
        }
    }
}
