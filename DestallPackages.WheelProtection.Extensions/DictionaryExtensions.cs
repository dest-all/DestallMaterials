using DestallMaterials.WheelProtection.Extensions.SpecialDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DestallMaterials.WheelProtection.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Соединяет словари по ключам первого словаря.
        /// Если в первом словаре есть ключи, которых нет во втором, при их выявлении будет выброшено KeyNotFoundException.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="dict1"></param>
        /// <param name="dict2"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TResult> JoinDictionaries<TKey, TValue1, TValue2, TResult>(this IDictionary<TKey, TValue1> dict1, IDictionary<TKey, TValue2> dict2, Func<TKey, TValue1, TValue2, TResult> selector)
        {
            var result = new Dictionary<TKey, TResult>();

            foreach (var kv in dict1)
            {
                result.Add(kv.Key, selector(kv.Key, kv.Value, dict2[kv.Key]));
            }
            return result;
        }

        /// <summary>
        /// Соединяет словари по ключам первого словаря. 
        /// Результирующий словарь будет заполнен полностью, даже если во втором словаре нет всех ключей, которые есть в первом.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="dict1"></param>
        /// <param name="dict2"></param>
        /// <param name="selector"></param>
        /// <param name="elementIfKeyNotFound"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TResult> JoinDictionariesSoft<TKey, TValue1, TValue2, TResult>(this IDictionary<TKey, TValue1> dict1, IDictionary<TKey, TValue2> dict2, Func<TKey, TValue1, TValue2, TResult> selector,
            Func<TKey, TValue1, TResult> elementIfKeyNotFound)
        {
            var result = new Dictionary<TKey, TResult>();
            foreach (var kv in dict1)
            {
                try
                {
                    result.Add(kv.Key, selector(kv.Key, kv.Value, dict2[kv.Key]));
                }
                catch (KeyNotFoundException)
                {
                    var value = elementIfKeyNotFound(kv.Key, kv.Value);
                    result.Add(kv.Key, value);
                }
            }
            return result;
        }

        public static MergedDictionary<TKey, TValue> Merge<TKey, TValue>(this IEnumerable<IDictionary<TKey, TValue>> dicts)
        {
            return new MergedDictionary<TKey, TValue>(dicts);
        }

        public static MergedDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dict, params IDictionary<TKey, TValue>[] dicts)
        {
            return new MergedDictionary<TKey, TValue>(new[] { dict }.Union(dicts));
        }

        /// <summary>
        /// Дополняет словарь ключами из множества. Значение опеределяется параметром-функцией от ключа. 
        /// Если ключ уже есть в словаре, пара ключ-значение не изменяется.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="keys"></param>
        /// <param name="valueGenerator"></param>
        public static void EnsureKeysArePresent<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<TKey> keys, Func<TKey, TValue> valueGenerator)
        {
            var absentKeys = keys.Except(dict.Keys).Distinct();
            foreach (var key in absentKeys)
            {
                dict.Add(key, valueGenerator(key));
            }
        }

        public static int RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> condition)
        {
            int result = 0;
            foreach (var key in dictionary.Where(condition)
                .Select(kv => kv.Key)
                .ToArray())
            {
                dictionary.Remove(key);
                result++;
            }
            return result;
        }
    }
}
