import React from 'react'

interface Props {
    onPortfolioDelete : (e:any) => void
    portfolioValue : string

}

const DeletePortfolio = ({onPortfolioDelete,portfolioValue}: Props) => {
  return <>
  <form onSubmit={onPortfolioDelete}>
  <input readOnly={true} hidden={true} value={portfolioValue}></input>
    <button type='submit'> X </button>
  </form>
  </>
    
  
}

export default DeletePortfolio