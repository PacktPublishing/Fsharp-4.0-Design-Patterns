let isVowel = function
              | 'A' | 'a' | 'E' | 'e' | 'I' | 'i'
              | 'O' | 'o' | 'U' | 'u' -> true
              | _ -> false

let alphabet = seq { 'A' .. 'Z' }

query {
    for letter in alphabet do
    where (isVowel letter)
    sortByDescending letter
    select letter // may be omitted
    head
}
// val it : char = 'U'

