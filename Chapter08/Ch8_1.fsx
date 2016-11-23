// QuickSort
let rec qsort : int list -> _ = function
   | [] -> []
   | x::xs ->
       let less, greater = List.partition ((>) x) xs
       qsort less @ x :: qsort greater

qsort [1;7;2;-5;-3;0;42]