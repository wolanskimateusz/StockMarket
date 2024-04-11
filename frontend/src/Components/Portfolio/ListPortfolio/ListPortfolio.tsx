import React from 'react'
import PortfolioCard from '../PortfolioCard/PortfolioCard'
import {v4 as uuidv4} from "uuid"

interface Props {
    portfolioValues: string[]
}

const ListPortfolio = ({portfolioValues}: Props) => {
  return <>
    <h3>My Portfolio</h3>
    <ul>
    {portfolioValues && portfolioValues.map((portfolioValue) => { return <PortfolioCard  key={uuidv4()} portfolioValue= {portfolioValue}/>})}
    </ul>
  </>
    
  
}

export default ListPortfolio