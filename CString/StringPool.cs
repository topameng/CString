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

public static class StringPool
{
    const int MaxSize = 1024;
    const int MaxQueueSize = 8;
    public static Dictionary<int, Queue<string>> map = new Dictionary<int, Queue<string>>();

    static public void PreAlloc(int size, int count)
    {
        if (size > MaxSize || size <= 0)
        {
            return;
        }

        count = Math.Max(MaxQueueSize, count);
        Queue<string> queue = null;

        if (map.TryGetValue(size, out queue))
        {
            for (int i = queue.Count; i < count; i++)
            {
                queue.Enqueue(new string((char)0xCC, size));
            }
        }
        else
        {
            queue = new Queue<string>();
            map[size] = queue;

            for (int i = 0; i < count; i++)
            {
                queue.Enqueue(new string((char)0xCC, size));
            }
        }
    }

    static public string Alloc(int size)
    {   
        if (size == 0)
        {
            return string.Empty;
        }

        if (size >= MaxSize)
        {
            return new string((char)0xCC, size);
        }
        
        Queue<string> queue = null;

        if (map.TryGetValue(size, out queue))
        {
            if (queue.Count > 0)
            {
                return queue.Dequeue();
            }
        }
        else
        {
            queue = new Queue<string>();
            map[size] = queue;
        }

        return new string((char)0xCC, size);
    }

    static public void Collect(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return;
        }

        int size = str.Length;

        if (size < MaxSize && size > 0)
        {
            Queue<String> queue = null;

            if (!map.TryGetValue(str.Length, out queue))            
            {
                queue = new Queue<string>();
                map[size] = queue;
            }

            if (queue.Count <= MaxQueueSize)
            {
                queue.Enqueue(str);
            }
        }
    }
}
