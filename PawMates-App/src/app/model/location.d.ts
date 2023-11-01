export interface Location {
    id?: number,
    petTypeId: number,
    name: string,
    street1: string,
    city: string,
    state: string,
    postalCode: string,
    petAge?: number
}