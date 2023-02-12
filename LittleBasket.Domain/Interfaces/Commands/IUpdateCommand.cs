using LittleBasket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Domain.Interfaces.Commands
{
    public interface IUpdateCommand<TModel>
    {
        Task Execute(TModel obj);
    }
}
