/// R E O R D E R I N G ///
List.sort [1;8;3;6;4;-2]
// val it : int list = [-2; 1; 3; 4; 6; 8]
List.sortDescending [1;8;3;6;4;-2]
// val it : int list = [8; 6; 4; 3; 1; -2]
List.sortBy (fun x -> x.GetHashCode()) ["Fourteen";"Zero";"Forty Two"]
// val it : string list = ["Zero"; "Forty Two"; "Fourteen"]

/// M A P P I N G ///
"Je ne regrette rien".Split([|' '|])
|> Seq.collect (fun x -> x.ToCharArray())
|> Seq.toList
// val it : char list =
//  ['J'; 'e'; 'n'; 'e'; 'r'; 'e'; 'g';
//   'r'; 'e'; 't'; 't'; 'e'; 'r'; 'i'; 'e'; 'n']

"Je ne regrette rien".Split([|' '|])
|> Seq.indexed
// val it : seq<int * string> =
//  seq [(0, "Je"); (1, "ne"); (2, "regrette"); (3, "rien")]

