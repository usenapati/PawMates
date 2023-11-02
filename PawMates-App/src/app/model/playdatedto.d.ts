import { Pet } from "./pet";

export interface PlayDateDTO {
    id: number,
    hostId: number,
    hostName: string,
    locationName: string,
    address: string,
    city: string,
    state: string,
    postalCode: string,
    eventName: string,
    eventDescription: string,
    startTime: Date,
    endTime: Date,
    numberOfPets: number,
    pets: Pet[]
}