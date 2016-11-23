// Imperative monolithic solution a-la C/C++
#load "HugeNumber.fs"
let number = hugeNumber.ToCharArray()

let mutable maxProduct = 0
let charZero = int('0')

for i in 0..995 do
    let mutable currentProduct = 1
    for j in 0..4 do
        currentProduct <- currentProduct * (int(number.[i + j]) - charZero)
    if maxProduct < currentProduct then
        maxProduct <- currentProduct

printfn "%s %d" "Imperative solution:" maxProduct