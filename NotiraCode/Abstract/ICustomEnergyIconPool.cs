using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Abstract;
public interface ICustomEnergyIconPool
{
    string? BigEnergyIconPath { get; }

    string? TextEnergyIconPath { get; }
}
