import { ChangeEvent, SyntheticEvent, useState } from 'react';
import './App.css';

import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';
import { CompanySearch } from './company';
import { searchCompanies } from './api';
import ListPortfolio from './Components/Portfolio/ListPortfolio/ListPortfolio';

function App() {
  
  const [search,setSearch] = useState<string>("")
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([])
  const [portfolioValues, setPortfolioValues] = useState<string[]>([])
  const [serverError, setServerError] = useState<string | null>(null)

  const onPortfolioCreate = (e : any) => {
    e.preventDefault()
    const exist = portfolioValues.find((value)=> value === e.target[0].value)
    if(exist) return
    const updatedPortfolio = [...portfolioValues, e.target[0].value]
    setPortfolioValues(updatedPortfolio)
    
  }

    const handleSearchChange = (e : ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value)
        console.log(e)
       
    }

    const onSearchSubmit = async (e: SyntheticEvent) => {
        e.preventDefault()
        const result = await searchCompanies(search)
        if(typeof result === "string"){
          setServerError(result)
          console.log(serverError)
        }else if(Array.isArray(result.data))
          {
            setSearchResult(result.data)
          }
          console.log(searchResult)

    }

  return (
    <div className="App">
      <Search onSearchSubmit={onSearchSubmit} handleSearchChange={handleSearchChange} search={search}/>
      <ListPortfolio portfolioValues ={portfolioValues}/>
      <CardList searchResult={searchResult} onPortfolioCreate={onPortfolioCreate}/>
    </div>
  );
}

export default App;
