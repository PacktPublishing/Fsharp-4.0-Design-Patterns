/// P A R T I T I O N I N G ///
List.chunkBySize 2 ['a'..'g']
// val it : char list list = [['a'; 'b']; ['c'; 'd']; ['e'; 'f']; ['g']]

List.groupBy (fun n -> n / 3) [1..7]
// val it : (int * int list) list = [(0, [1; 2]); (1, [3; 4; 5]); (2, [6; 7])]

List.pairwise [1..2..10]
// val it : (int * int) list = [(1, 3); (3, 5); (5, 7); (7, 9)]

["angle";"delta";"cheese";"America"]
|> List.partition (fun (x:string) -> (System.Char.ToUpper x.[0]) = 'A')
// val it : string list * string list =
//  (["angle"; "America"], ["delta"; "cheese"])

["angle";"delta";"cheese";"America"]
|> List.splitAt 2
// val it : string list * string list =
//  (["angle"; "delta"], ["cheese"; "America"])

["angle";"delta";"cheese";"America"]
|> List.splitInto 3
// val it : string list list =
//   [["angle"; "delta"]; ["cheese"]; ["America"]]

["angle";"delta";"cheese";"America"]
|> List.windowed 2
// val it : string list list =
//   [["angle"; "delta"]; ["delta"; "cheese"]; ["cheese"; "America"]]