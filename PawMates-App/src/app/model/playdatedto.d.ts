import { Pet } from "./pet";

export interface PlayDateDTO {
    hostName: string,
    locationName: string,
    eventName: string,
    eventDescription: string,
    startTime: Date,
    endTime: Date,
    numberOfPets: number,
    pets: Pet[]
}