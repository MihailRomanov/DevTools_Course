using System;
using System.Collections;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
    /// <summary>
    /// Простой пример реализации структуры данных стек нативными средствами языка C#.
    /// </summary>
    public class Stack<T> : IEnumerable<T>
    {
        private readonly T[] _Array;

        /// <summary>
        /// Маркер верхнего элемента стека
        /// </summary>
        private int Peak { get; set; }

        /// <summary>
        /// Размер стека
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// Количество элементов стека
        /// </summary>
        public int Count
        {
            get
            {
                return Peak;
            }
        }

        /// <summary>
        /// Конструктор. Создание стек
        /// </summary>
        public Stack(int size)
        {
            Capacity = size;
            Peak = 0;
            _Array = new T[Capacity];
        }

        /// <summary>
        /// Проверить заполнен ли стек
        /// </summary>
        public bool IsFull()
        {
            return Peak == Capacity;
        }

        /// <summary>
        /// Проверить пустой ли стек
        /// </summary>
        public bool IsEmpty()
        {
            return Peak == 0;
        }

        /// <summary>
        /// Добавляем элемент в стек
        /// </summary>
        public void Push(T item)
        {
            if (IsFull())
                throw new InvalidOperationException("Стек переполнен. Невозможно добавить элемент.");

            _Array[Peak++] = item;
        }

        /// <summary>
        /// Считывание из стека верхнего элемента и удаление этого элемента
        /// </summary>
        public T Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Стек пуст. Нет элементов для получения.");
            }
            return _Array[--Peak];
        }

        /// <summary>
        /// Считывание из стека верхнего элемента
        /// </summary>

        public T Top()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Стек пуст. Нет элементов для получения.");
            }
            return _Array[Peak - 1];
        }

        /// <summary>
        /// Считываем стек "от вершины к низу"
        /// </summary>

        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty()) yield break;

            for (int i = Peak; i > 0; i--)
                yield return _Array[i - 1];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}