import React from 'react'

interface Props {
    onPortfolioDelete : (e:any) => void
    portfolioValue : string

}

const DeletePortfolio = ({onPortfolioDelete,portfolioValue}: Props) => {
  return <>
  <form onSubmit={onPortfolioDelete}>
  <input readOnly={true} hidden={true} value={portfolioValue}></input>
  <button className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500">
          X
        </button>
  </form>
  </>
    
  
}

export default DeletePortfolio