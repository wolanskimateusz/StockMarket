import React from 'react'
import Card from '../Card'

type Props = {}

const CardList = (props: Props) => {
  return (
    <div>
        <Card companyName="Apple" ticker="APPL" price={100} />
        <Card companyName="Microsoft" ticker="MSFT" price={200} />
        <Card companyName="Tesla" ticker="TSLA" price={300} />
        
    </div>
  )
}

export default CardList