/*
Copyright (c) 2016-2017 topameng(topameng@qq.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;

public class ArrayPool<T>
{
    public const int MAX_COUNT = 16;
    Queue<T[]>[] pool = new Queue<T[]>[MAX_COUNT];

    public ArrayPool()
    {
        for (int i = 0; i < MAX_COUNT; i++)
        {
            pool[i] = new Queue<T[]>();
        }
    }    

    public int NextPowerOfTwo(int v)
    {
        v -= 1;
        v |= v >> 16;
        v |= v >> 8;
        v |= v >> 4;
        v |= v >> 2;
        v |= v >> 1;
        return v + 1;
    }

    public T[] Alloc(int n)
    {        
        int size = NextPowerOfTwo(n);
        int pos = GetSlot(size);

        if (pos >= 0 && pos < MAX_COUNT)
        {
            Queue<T[]> queue = pool[pos];
            int count = queue.Count;

            if (count > 0)
            {
                return queue.Dequeue();
            }

            return new T[size];
        }
        
        return new T[n];
    }

    public void Collect(T[] buffer)
    {
        if (buffer == null) return;        
        int pos = GetSlot(buffer.Length);

        if (pos >= 0 && pos < MAX_COUNT)
        {
            Queue<T[]> queue = pool[pos];
            queue.Enqueue(buffer);
        }
    }

    int GetSlot(int value)
    {
        int len = 0;

        while (value > 0)
        {
            ++len;
            value >>= 1;
        }

        return len;
    }
}