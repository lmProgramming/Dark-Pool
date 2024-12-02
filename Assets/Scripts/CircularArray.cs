using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoCircularArray<T>
{
    T[] array;
    int pointer = 0;
    public int Length { get; private set; }

    public PseudoCircularArray(int length) 
    {
        array = new T[length];
        pointer = 0;
        Length = length;
    }

    public void FillWith(T value)
    {
        for (int i = 0; i < Length; i++)
        {
            array[i] = value;
        }
    }

    public void ReplaceLast(T value)
    {
        array[pointer] = value;
        pointer++;
        if (pointer == Length)
        {
            pointer = 0;
        }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= array.Length)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            return array[index];
        }
        set
        {
            if (index < 0 || index >= array.Length)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            array[index] = value;
        }
    }
}
