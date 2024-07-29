export class User {
    id!: number
    registrationNumber!: number
    firstName!: string
    lastName!: string
    passwordSalt!:string[]
    passwordHash!:string[]
    isActive!: boolean
}
