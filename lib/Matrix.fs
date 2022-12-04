module Matrix

open System

type Matrix<'a> = 'a list list

let map (f: 'a -> 'b) (m: Matrix<'a>): Matrix<'b> = List.map (List.map f) m

let mapi (f: int -> int -> 'a -> 'b) (m: Matrix<'a>): Matrix<'b> =
    List.mapi (fun x -> List.mapi (f x)) m

let tryItem (x: int, y: int) (m: Matrix<'a>) =
    List.tryItem x m
    |> Option.bind (List.tryItem y)

let hasPosition (x: int, y: int) (m: Matrix<'a>) = 
    x >= 0 && y >= 0 && x < m.Length && y < m.[x].Length

let tryFindIndex f (m: Matrix<'a>) =
    m
    |> List.indexed
    |> List.choose (fun (idxX, l) ->
        match List.tryFindIndex f l with
        | Some idxY -> Some (idxX, idxY)
        | None -> None
    )
    |> List.tryHead

let indexed (m: Matrix<'a>): Matrix<int * int * 'a> =
    m
    |> List.map (List.indexed)
    |> List.indexed
    |> List.map (fun (x, inner) -> List.map (fun (y, v) -> (x, y, v)) inner)

let fold (f: 'state -> 'a -> 'state) (state: 'state) (m: Matrix<'a>) =
    let mutable s = state
    for row in m do
        for el in row do
            s <- f s el
    s

let lastIndex (m: Matrix<'a>) =
    let lastX = m.Length-1

    if lastX < 0 then
        raise (ArgumentException ("Cannot get last index of matrix with width 0"))

    let lastY = m.[lastX].Length - 1

    if lastX < 0 then
        raise (ArgumentException ("Cannot get last index of matrix with height 0"))

    (lastX, lastY)
