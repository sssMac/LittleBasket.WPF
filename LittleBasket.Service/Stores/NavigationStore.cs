using LittleBasket.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Service.Stores
{
    //Хранит в себе текущую страницу
    public class NavigationStore 
    {
        //Текущая страница
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        //Ивент на изменение текущей страницы
        public event Action CurrentViewModelChanged;

		//тригириться при изменении CurrentViewModel и вызывает ивент
		private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
