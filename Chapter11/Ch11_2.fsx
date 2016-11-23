#r @"C:\code\packtbook\KeyTypeProvider\bin\Debug\KeyTypeProvider.dll"
open FSharp.IO
open System

type Vault = SecretKey< @".\Secret.txt">

let unlock = function
| Vault.Key -> true
| _ -> false

while Console.ReadLine() |> unlock |> not do
    printfn "Go away, Hacker!"

printfn "Please proceed, Master!"
