// Object-oriented solution a-la C# with Iterator pattern
#load "HugeNumber.fs"

open System
open System.Collections.Generic

type OfDigits(digits: char[]) =
    let mutable product = 1
    do
        if digits.Length > 9 then // (9 ** 10) > Int32.MaxValue
            raise <| ArgumentOutOfRangeException("Constrained to max 9 digit numbers")
        let charZero = int '0' in
        for d in digits do
            product <- product * ((int d) - charZero)
    member this.Product
        with get() = product

type SequenceOfDigits(digits: string, itemLen: int) =
    let collection: OfDigits[] = Array.zeroCreate (digits.Length - itemLen + 1)
    do
        for i in 0 .. digits.Length - itemLen do
            collection.[i] <- OfDigits(digits.[i..(i+itemLen-1)].ToCharArray())
    member this.GetEnumerator() =
        (collection :> IEnumerable<OfDigits>).GetEnumerator()

let mutable maxProduct = 1
for item in SequenceOfDigits(hugeNumber,5) do
    maxProduct <- max maxProduct item.Product

printfn "%s %d" "Object-oriented solution:" maxProduct