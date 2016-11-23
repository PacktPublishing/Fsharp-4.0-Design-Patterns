// Converting from recursive integerPow
let rec power (value: double) = function
    | neg when neg < 0 -> 1./power value (-neg)
    | 0 -> 1.
    | 1 -> value
    | pow -> match pow % 2 with
                | 0 -> let half = (power value (pow / 2)) in half * half
                | _ -> let half = (power value (pow / 2)) in value * half * half

// to folding
let intPow ``base`` exp =
    seq {
        let exp = ref exp in
        while !exp > 0 do
            yield !exp &&& 1
            exp := !exp >>> 1    
    }
    |> Seq.fold (fun (b,r) i -> if i = 1 then (b*b, r * b) else (b * b, r)) (``base``,1)
    |> snd

// to unfold
let intPow'' ``base`` exp =
    Seq.unfold (
        function
        | (_,_,0) -> None
        | (result,``base``,exp) ->
            let ``base*base``,halfexp = ``base``*``base``,exp >>> 1 in
            if exp &&& 1 = 1 then
                let ``result*base`` = result * ``base`` in
                Some(``result*base``,(``result*base``,``base*base``,halfexp))
            else 
                Some(result,(result,``base*base``,halfexp))
        )
        (1,``base``,exp)
    |> Seq.last






