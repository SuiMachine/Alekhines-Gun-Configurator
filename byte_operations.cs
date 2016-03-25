using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlekhinesGunConfigurator
{
    class byte_operations
    {
        byte[] array;

        public byte_operations(byte[] _array)
        {
            array = _array;
        }

        public int findAdress(byte[] signature, int byteOffset)
        {
            for (int adress = 0; adress < array.Length; adress++)
            {
                if (compareByteArrays(signature, array, adress))
                {
                    return adress + byteOffset;
                }
            }
            return 0;
        }

        private bool compareByteArrays(byte[] sequenceArray, byte[] dataArray, int lookFrom)
        {
            if (dataArray.Length - lookFrom > sequenceArray.Length)
            {
                for (int i = 0; i < sequenceArray.Length; i++)
                {
                    if (sequenceArray[i] != dataArray[lookFrom + i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public void replaceBytes(byte[] data, int address)
        {
            if(address+data.Length<array.Length)
            {
                for(int i=0; i<data.Length; i++)
                {
                    array[address + i] = data[i];
                }
            }
        }

        public void replaceBytes(byte data, int address)
        {
            if (address < array.Length)
            {
                array[address] = data;
            }
        }

        public byte[] returnArray()
        {
            return array;
        }

    }
}
