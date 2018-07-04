using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Party_2018 {

  public class EntityCounter {

    public int currentCount;

    // Constructor:
    public EntityCounter() {
      currentCount = 0; // starts at zero
    }


    // Read current number:
    public int readCount() {
      return currentCount;
    }

    // Take number:
    public int takeNumber() {
      currentCount++;
      return currentCount - 1; // return previous after increment
    }


  } // end class definition
}
