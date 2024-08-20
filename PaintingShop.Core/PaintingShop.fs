namespace PaintingShop.Core

open System

type PictureState = None | Delete

type Picture = 
    {
        pictureId : uint
        name : string
        date : DateTime
        state : PictureState
    }

type Client = 
    {
        clientId : uint
        name : string
    }

type ReserveState = Active | InActive

type Reserve = 
    {
        reserveId : uint
        pictureId : uint
        clientId : uint
        date : DateTime
        state : ReserveState
    }

module PaintingShop =

    let addPicture name : Picture =
        {
            pictureId = 0u
            name = name
            state = None
            date = DateTime.UtcNow
        }

    let deletePicture (picture : Picture) =
        { 
            picture with  state = Delete 
        }

    let reservePicture picture client = 
        {
            reserveId = 0u
            pictureId = picture.pictureId
            clientId = client.clientId
            date = DateTime.UtcNow
            state = Active
        }

    let cancelReservePicture (reserve : Reserve) =
        {
            reserve with state = InActive
        }