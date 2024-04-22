import React from 'react'
import Table from '../../Components/Table/Table'
import RatioList from '../../Components/RatioList/RatioList'
import { TestDataCompany } from '../../Components/Table/TestData'

interface Props  {
}

const data = TestDataCompany
const tableConfig = [
  {label : "symbol",
  render : (company : any) => company.symbol,
}
]

const DesignPage = (props: Props) => {
  return (
    <>
    <h1>Design Page</h1>
    <RatioList config={tableConfig} data={data} />
    <Table config ={tableConfig} data={data}/>
    </>
  )
}

export default DesignPage