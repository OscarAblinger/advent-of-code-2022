module Extensions

let (|LargerThan|Equal|LessThan|) (a, b) =
    match compare a b with
    | 0 -> Equal
    | x when x > 0 -> LargerThan
    | _ -> LessThan

module List =
    let countInt64 (list: 'a list): Map<'a, int64> =
        list
        |> List.fold (fun map value ->
            Map.change value (fun cnt ->
                match cnt with
                | Some c -> Some (c + 1L)
                | None -> Some 1) map
        ) Map.empty
        
    let mergePairsBy (mergeFunc: 'b -> 'b -> 'b) (list: ('a * 'b) list): ('a* 'b) list =
        list
        |> List.groupBy fst
        |> List.map (fun (key, values) -> (key, values |> List.map snd |> List.reduce mergeFunc))

    let zipWith (f: 'a -> 'b) (list: 'a list): ('a * 'b) list =
        List.map (fun el -> el, f el) list
    
    let zipWithSome (f: 'a -> 'b option) (list: 'a list): ('a * 'b) list =
        List.choose (fun el ->
            match f el with
            | Some v -> Some (el, v)
            | None -> None
        ) list

module Seq =
    /// Splits a sequence into subsequences at all elements that the given predicate returns true for
    /// The triggering element is included in the following subsequence
    let splitWith f input =
        let i = ref 0
        input
        |> Seq.groupBy (fun s -> (if f s then incr i) ; !i)
        |> Seq.map snd

    /// Splits a sequence into subsequence at all elements that the given predicate returns true for
    /// The triggering element is not included in any subsequence
    let splitWhen f input =
        let i = ref 0
        input
        |> Seq.choose (fun s ->
                        if f s then
                            incr i
                            None
                        else
                            Some (!i, s))
        |> Seq.groupBy fst
        |> Seq.map (snd >> Seq.map snd)

module Map =
    let mapValues f (map: Map<_, _>) = Map.map (fun _ values -> f values) map

    let merge (convert: 'b -> 'b -> 'b) (m1: Map<'a, 'b>) (m2: Map<'a, 'b>): Map<'a, 'b> =
        Map.fold (fun acc key value ->
            Map.change key (fun el ->
                el
                |> Option.map (convert value)
                |> Option.orElse (Some value)
            ) acc
        ) m2 m1

module MultiMap =
    let union (m1: Map<'a, Set<'b>>) (m2: Map<'a, Set<'b>>) =
        Map.fold (fun acc key value ->
            Map.change key (fun el ->
                    el
                    |> Option.map (Set.union value)
                    |> Option.orElse (Some value)
                ) acc
            ) m1 m2

    let intersection (m1: Map<'a, Set<'b>>) (m2: Map<'a, Set<'b>>) =
        Map.fold (fun acc key value ->
            Map.change key (fun el ->
                    el
                    |> Option.map (Set.intersect value)
                    |> Option.bind (fun s -> if s.IsEmpty then None else Some s)
                ) acc
            ) m1 m2

module Option =
    let orElseFail (errMsg: string) (option: Option<'a>): 'a =
        match option with
        | Some v -> v
        | None -> failwith errMsg
