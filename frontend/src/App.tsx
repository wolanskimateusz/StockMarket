import { ChangeEvent, SyntheticEvent, useState } from 'react';
import './App.css';

import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';
import { CompanySearch } from './company';
import { searchCompanies } from './api';

function App() {
  
  const [search,setSearch] = useState<string>("")
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([])
  const [serverError, setServerError] = useState<string>("")

  const onPortfolioCreate = (e : SyntheticEvent) => {
    e.preventDefault()
    console.log(e)
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
      <CardList searchResult={searchResult} onPortfolioCreate={onPortfolioCreate}/>
    </div>
  );
}

export default App;
