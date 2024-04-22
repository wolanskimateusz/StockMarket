import React from 'react'
import { testIncomeStatementData } from './TestData'

const data = testIncomeStatementData

interface Props {}

type Company = (typeof data)[0]

const configs = [
    {
        label: "Year",
        render: (company:Company) => company.acceptedDate

    },
    {
        label: "Cost of revenue",
        render: (company:Company) => company.costOfRevenue
    }
]

const Table = (props: Props) => {

    const renderedRows = data.map((company) => {
        return (
            <tr key={company.cik}>
                {configs.map((value : any) => {
                   return <td className="p-4 whitespace-nowrap text-sm font-normal text-gray-900">
                    {value.render(company)} </td>
                })}
                
            </tr>
        )
    })


    const renderedHeaders = configs.map((config :any )=>{
        return <th className="p-r text-left text-xs font-medium text-fray-500 uppercase tracking-wider" key={config.label}>
            {config.label}
        </th>
    })
  return (
    <div className="bg-white shadow rounded-lg p-4 sm:p-86 xl:p-8">
        <table>
            <thead className="min-w-full divide-y divide=gray-200 m-5">{renderedHeaders}</thead>
            <tbody>{renderedRows}</tbody>
        </table>
    </div>
  )
}

export default Table