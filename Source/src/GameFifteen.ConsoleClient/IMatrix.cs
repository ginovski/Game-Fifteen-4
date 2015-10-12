using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen.ConsoleClient
{
    public interface IMatrix
    {
        void InitializeMatrix();
        string this[int row, int column] { get; set; }
    }
}
