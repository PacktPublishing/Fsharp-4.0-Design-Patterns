let isVowel = function
              | 'A' | 'a' | 'E' | 'e' | 'I' | 'i' 
              | 'O' | 'o' | 'U' | 'u' -> true
              | _ -> false

let alphabet = seq { 'A' .. 'Z' }

alphabet |> Seq.filter isVowel |> Seq.sortDescending |> Seq.head
// val it : char = 'U' 

