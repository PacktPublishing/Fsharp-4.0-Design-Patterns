open System.Linq

let isVowel = function
              | 'A' | 'a' | 'E' | 'e' | 'I' | 'i'
              | 'O' | 'o' | 'U' | 'u' -> true
              | _ -> false

let alphabet = seq { 'A' .. 'Z' }

alphabet.Where(isVowel).OrderByDescending(fun x -> x).First()
// val it : char = 'U'
