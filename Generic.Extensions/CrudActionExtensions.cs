using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generic.Extensions
{
    public static class CrudActionExtensions
    {
        public static IList<T> Set<T>(this IContainer containerObject) // Pobiera listę z obiektu kontenerowego na podstawie właściwości
        {
            var collectionProperty = containerObject.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                // Jeśli tak, zwracamy tę listę
                return (IList<T>)collectionProperty.GetValue(containerObject);
            }

            // Jeśli nie znaleziono właściwości, zwracamy null
            return null;
        }

        public static void ForEach<T>(this IList<T> list, Action<T> action)  // Wykonuje akcję dla każdego elementu na liście
        {
            var collectionProperty = list.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy lista posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                var collection = (IList<T>)collectionProperty.GetValue(list); // Jeśli tak, uzyskujemy dostęp do listy

                foreach (var item in collection) // Wykonujemy podaną akcję dla każdego elementu na liście
                {
                    action(item);
                }
            }
        }

        public static T Get<T>(this IContainer container, Func<T, bool>? searchPredicate = null) // Pobranie elementu z obiektu kontenerowego na podstawie predykatu wyszukiwania
        {
            var collectionProperty = container.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                var collection = (IList<T>)collectionProperty.GetValue(container); // Jeśli tak, uzyskujemy dostęp do listy

                // Jeśli podano predykat wyszukiwania, zwracamy pierwszy pasujący element, w przeciwnym razie pierwszy element listy
                if (searchPredicate != null)
                {
                    return collection.FirstOrDefault(searchPredicate);
                }
                else
                {
                    return collection.FirstOrDefault();
                }
            }

            return default(T); // Jeśli nie znaleziono właściwości lub listy, zwracamy domyślną wartość dla typu T
        }

        public static IList<T> GetList<T>(this IContainer container, Func<T, bool>? searchPredicate = null) // Pobranie listy elementów z obiektu kontenerowego na podstawie predykatu wyszukiwania
        {
            var collectionProperty = container.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                var collection = (IList<T>)collectionProperty.GetValue(container); // Jeśli tak, uzyskujemy dostęp do listy

                // Jeśli podano predykat wyszukiwania, zwracamy listę pasujących elementów, w przeciwnym razie całą listę
                if (searchPredicate != null)
                {
                    return collection.Where(searchPredicate).ToList();
                }
                else
                {
                    return collection.ToList();
                }
            }

            // Jeśli nie znaleziono właściwości lub listy, zwracamy nową pustą listę dla typu T
            return new List<T>();
        }

        public static T Add<T>(this IContainer container, T obj) // Dodanie elementu do obiektu kontenerowego
        {
            var collectionProperty = obj.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                // Jeśli tak, uzyskujemy dostęp do listy i dodajemy do niej nowy element
                var collection = (IList<T>)collectionProperty.GetValue(obj);
                collection.Add(obj);
            }

            return obj; // Zwracamy dodany obiekt
        }

        public static bool Remove<T>(this IContainer container, Func<T, bool> searchFn) // Usunięcie elementów z obiektu kontenerowego na podstawie funkcji wyszukiwania
        {
            var collectionProperty = container.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                var collection = (IList<T>)collectionProperty.GetValue(container); // Jeśli tak, uzyskujemy dostęp do listy
                var itemsToRemove = collection.Where(searchFn).ToList(); // Znajdujemy elementy do usunięcia na podstawie funkcji wyszukiwania

                // Usuwamy znalezione elementy z listy
                foreach (var item in itemsToRemove)
                {
                    collection.Remove(item);
                }

                // Zwracamy informację, czy cokolwiek zostało usunięte
                return itemsToRemove.Count > 0;
            }

            // Jeśli nie znaleziono właściwości lub listy, zwracamy false
            return false;
        }

        public static IContainer AddRange<T>(this IContainer container, IList<T> listOfElements) // Dodanie zakresu elementów do obiektu kontenerowego
        {
            var collectionProperty = container.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                // Jeśli tak, uzyskujemy dostęp do listy i dodajemy do niej cały zakres elementów
                var collection = (IList<T>)collectionProperty.GetValue(container);
                foreach (var item in listOfElements)
                {
                    collection.Add(item);
                }
            }

            // Zwracamy obiekt kontenerowy po dodaniu zakresu elementów
            return container;
        }
    }
}