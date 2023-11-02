export interface PlayDate {
    id: number,
    petParentId: number,
    locationId: number,
    eventTypeId: number,
    startTime: Date,
    endTime: Date,
    hostPets: number[],
    invitedPets: number[]
}