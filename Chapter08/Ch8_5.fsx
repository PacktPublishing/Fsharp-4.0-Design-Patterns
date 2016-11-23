/// SELECTION ///
List.head<int> []
// System.ArgumentException: The input list was empty.
List.tryHead<int> []
// val it : int option = None

let ll = [1;2;3;4]
List.head ll = ll.[0]
//val it : bool = true

let aa = [|1;2;3;4|]
Array.get aa 2 = aa.[2] 
// val it : bool = true

[|10;20;30;40;50;60|].[2..4]
// val it : int [] = [|30; 40; 50|]

let numbers = [1;2;3;4;5;6;7;8]
List.filter (fun x -> (%) x 2 = 0) numbers = List.where (fun x -> (%) x 2 = 0) numbers
// val it : bool = true

List.find (fun x -> (%) x 2 = 0) <| [1;3;5]
// System.Collections.Generic.KeyNotFoundException:
// Exception of type 'System.Collections.Generic.KeyNotFoundException' was thrown.
List.tryFind (fun x -> (%) x 2 = 0) <| [1;3;5]
// val it : int option = None
List.find (fun x -> (%) x 2 <> 0) <| [1;3;5]
// val it : int = 1
List.tryFind (fun x -> (%) x 2 <> 0) <| [1;3;5]
// val it : int option = Some 1
List.findIndex (fun x -> (%) x 2 <> 0) <| [1;3;5]
// val it : int = 0
List.tryFindIndex (fun x -> (%) x 2 <> 0) <| [1;3;5]
// val it : int option = Some 0
List.findBack (fun x -> (%) x 2 <> 0) <| [1;3;5]
// val it : int = 5
List.tryFindBack (fun x -> (%) x 2 <> 0) <| [1;3;5]
// val it : int option = Some 5
List.findIndexBack (fun x -> (%) x 2 <> 0) <| [1;3;5]
// val it : int = 2
List.tryFindIndexBack (fun x -> (%) x 2 <> 0) <| [1;3;5]
// val it : int option = Some 2

[(9,"Nine");(42,"FortyTwo");(0,"Zero")]
|> List.pick (fun (x,y) -> if x = 42 then Some y else None)
// val it : string = "FortyTwo"
[(9,"Nine");(42,"FortyTwo");(0,"Zero")]
|> List.tryPick (fun (x,y) -> if x = 42 then Some y else None)
// val it : string option = Some "FortyTwo"
[(9,"Nine");(42,"FortyTwo");(0,"Zero")]
|> List.pick (fun (x,y) -> if x = 14 then Some y else None)
// System.Collections.Generic.KeyNotFoundException:
// Exception of type 'System.Collections.Generic.KeyNotFoundException' was thrown.
[(9,"Nine");(42,"FortyTwo");(0,"Zero")]
|> List.tryPick (fun (x,y) -> if x = 14 then Some y else None)
// val it : string option = None