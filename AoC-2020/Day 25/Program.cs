using System;

namespace Day_25
{
    class Program
    {
        static void Main(string[] args)
        {
            long cardPublicKey = 17773298;
            long doorPublicKEy = 15530095;
            PartOne(cardPublicKey, doorPublicKEy);
            // No part two
        }

        static void PartOne(long cardPublicKey, long doorPublicKey)
        {
            long doopLoopSize = GetLoopSize(doorPublicKey);
            long encryptionKey = GetEncryptionKey(cardPublicKey, doopLoopSize);
            Console.WriteLine($"Part one: {encryptionKey}");
        }

        static long GetLoopSize(long publicKey)
        {
            long result = 1;
            for(long i = 1; ; i++)
            {
                result = (result * 7) % 20201227;
                if (result == publicKey) { return i; }
            }
        }

        static long GetEncryptionKey(long publicKey, long loopSize)
        {
            long result = 1;
            for(int i = 0; i < loopSize; i++)
            {
                result = (result * publicKey) % 20201227;
            }
            return result;
        }
    }
}
