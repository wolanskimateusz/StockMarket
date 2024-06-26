import axios from "axios"
import { CompanyIncomeStatement, CompanyKeyMetrics, CompanyProfile, CompanySearch } from "./company"

interface SearchResponse{
    data : CompanySearch[]
}

export const searchCompanies = async (query : string) =>{
    try{
        const data = await axios.get<SearchResponse>(
            `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&exchange=NASDAQ&apikey=${process.env.REACT_APP_API_KEY}`
        )
        return data
    } catch(error){
        if(axios.isAxiosError(error)) {
            console.log("Error message: ", error.message)
            return error.message
        } else {
            console.log("Unexpected error: ", error)
            return "Unexpeted error has occured!"
        }

    }

  
}

export const getCompanyProfile = async (query: string) => {
    try{
        const data = await axios.get<CompanyProfile[]>(
            `https://financialmodelingprep.com/api/v3/profile/${query}?limit=40&apikey=${process.env.REACT_APP_API_KEY}`
        ) 
        return data
    } catch(error : any){
        console.log("error message from API: ", error)
    }
}

export const getKeyMetrics = async (query: string) => {
    try{
        const data = await axios.get<CompanyKeyMetrics[]>(
            `https://financialmodelingprep.com/api/v3/key-metrics-ttm/${query}?limit=40&apikey=${process.env.REACT_APP_API_KEY}`
        ) 
        return data
    } catch(error : any){
        console.log("error message from API: ", error)
    }
}

export const getIncomeStatement = async (query: string) => {
    try{
        const data = await axios.get<CompanyIncomeStatement[]>(
            `https://financialmodelingprep.com/api/v3/income-statement/${query}?limit=40&apikey=${process.env.REACT_APP_API_KEY}`
        ) 
        return data
    } catch(error : any){
        console.log("error message from API: ", error)
    }
}

