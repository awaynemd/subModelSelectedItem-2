namespace DataGrid2

open System
open Elmish
open Elmish.WPF


module Cell =
    type Model =
        { CellName: string
          Id: Guid }

    let init j i =
        { CellName = sprintf "CellName %i  %i" j i
          Id = new Guid()
        }

    type Msg =
        | Select of Guid option

    let bindings() = [
        "CellName" |> Binding.oneWay( fun v -> v.CellName)
       // "SelectedLabel" |> Binding.oneWay (fun (m, e) -> if m.Selected = Some e.Id then " - SELECTED" else "")
        "SelectedLabel" |> Binding.oneWay (fun v -> " -- HI!")
     ]


module Row =

    type Model =
      { RowName: string
        Columns: Cell.Model list
        Id: Guid }

    type Msg =
         | Select of Guid option
         | NoOp

    let init i =
        { RowName = sprintf "RowName %i" i
          Columns =  [0 .. 2] |> List.map (fun j -> Cell.init j i)
          Id = new Guid()
        }

    let bindings() = [
           "RowName" |> Binding.oneWay( fun v -> v.RowName)
           "Columns" |> Binding.subModelSeq(
               (fun m -> m.Columns),
               snd,
               (fun e -> e.Id),
               (fun _ -> NoOp),
               Cell.bindings ) 
               
        //   "SelectedEntity" |> Binding.subModelSelectedItem("Columns", (fun m -> m.Selected), Select)
    ]

    
module App =  

   type Model =
      { Rows: Row.Model list
        Selected: Guid option }

   let init () =
     {  Rows = [0 .. 2] |> List.map (fun i -> Row.init i)
        Selected = None}

   type Msg =
      | Select of Guid option
      | NoOp

   let update msg m =
      match msg with
      | Select entityId -> { m with Selected = entityId }
      | NoOp -> m

   let bindings () : Binding<Model, Msg> list = [     
        "Rows" |> Binding.subModelSeq(
            (fun m -> m.Rows),
            snd,
            (fun e -> e.Id),
            (fun _ -> NoOp),
            Row.bindings )      
   ]

   let main window =
      Program.mkSimpleWpf init update bindings
      |> Program.withConsoleTrace
      |> Program.runWindowWithConfig
        { ElmConfig.Default with LogConsole = true; Measure = true }
        window


 