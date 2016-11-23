// Active Patterns
open System

let (|Echo|) x = x

let checkEcho p = 
    match p with
    | Echo 42 -> "42!"
    | Echo x -> sprintf "%O is not good" x

checkEcho 0
checkEcho 42
checkEcho "echo" // Does not compile!

let (|``I'm active pattern``|) x = x + 2

let x = match 40 with ``I'm active pattern`` x -> x

let (``I'm active pattern`` x) = 40

let hexCharSet = ['0'..'9'] @ ['a'..'f'] |> set in
let (|IsValidGuidCode|) (guidstr: string) =
    let (|HasRightSize|) _ = guidstr.Length = 32
    let (|IsHex|) _ = (guidstr.ToLower() |> set) = hexCharSet
    match () with (HasRightSize rightsize & IsHex hex)-> rightsize && hex

let (IsValidGuidCode valid) = "abc"
let (IsValidGuidCode valid) = "0123456789AbCdEfFFEEDDCCbbAA9988"





