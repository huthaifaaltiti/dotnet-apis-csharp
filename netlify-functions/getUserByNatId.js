// netlify-functions/getUserByNatId.js

import fetch from "node-fetch";

exports.handler = async function (event, context) {
  const { userNatNum } = event.queryStringParameters;

  try {
    const response = await fetch(
      `/.netlify/netlify-functions/getUserByNatId?userNatNum=${userNatNum}`
    );
    const data = await response.json();

    return {
      statusCode: 200,
      body: JSON.stringify(data),
    };
  } catch (error) {
    return {
      statusCode: 500,
      body: JSON.stringify({ error: "Internal Server Error" }),
    };
  }
};
