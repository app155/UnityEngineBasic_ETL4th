using System.ComponentModel;
using System.Reflection;

namespace Attributes
{
    internal class Binder<T> 
    {
        private readonly T _receiver;
        private readonly PropertyDescriptorCollection _sourceProperties;
        private readonly PropertyDescriptorCollection _receiverMappedProperties;

        /// <summary>
        /// </summary>
        /// <param name="receiver"> 알림을 받을 객체 (View) </param>
        /// <param name="source"> 알림을 고지하는 데이터 (Model) </param>
        /// <param name="tag"> 소스를 이름 외에도 구분하기위한 태그 </param>
        public Binder(T receiver, INotifyPropertyChanged source, SourceTag tag)
        {
            _receiver = receiver;

            PropertyDescriptorCollection receiverProperties = TypeDescriptor.GetProperties(_receiver);
            _sourceProperties = TypeDescriptor.GetProperties(source);

            source.PropertyChanged += OnSourcePropertyChanged;

            _receiverMappedProperties = new PropertyDescriptorCollection(null);

            // BindAttribute를 가지고 있으면서, 태그가 일치하는 Receiver의 Property를 저장.
            foreach (PropertyDescriptor property in receiverProperties)
            {
                BindAttribute? attribute = (BindAttribute)property.Attributes[typeof(BindAttribute)];

                if (attribute != null && attribute.Tag == tag)
                {
                    _receiverMappedProperties.Add(property);
                }
            }
        }

        /// <summary>
        /// 소스 (Sender)의 값이 바뀌었을 때, 매핑된 Receiver가 있다면, Sender의 Property의 값으로 Receiver의 Property값을 갱신.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyDescriptor property = _receiverMappedProperties[args.PropertyName];

            if (property != null)
            {
                property.SetValue(_receiver, _sourceProperties[args.PropertyName].GetValue(sender));
            }
        }
    }
}
